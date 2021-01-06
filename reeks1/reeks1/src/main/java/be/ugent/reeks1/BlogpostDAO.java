
package be.ugent.reeks1;

import java.util.ArrayList;

import org.springframework.stereotype.Service;

import be.ugent.Exceptions.BlogPostNotFoundException;

@Service
public class BlogpostDAO {

    private ArrayList<Blogpost> posts;

    public BlogpostDAO() {
        Blogpost post = new Blogpost("boe", "aaaaaah, ik was geschrokken");
        this.posts = new ArrayList<Blogpost>();
        this.posts.add(post);

    }

    public ArrayList<Blogpost> getAllBlogposts(){
        return posts;
    }

    public Blogpost getBlogpostById(int id) throws BlogPostNotFoundException{
        for(Blogpost post: this.posts){
            if(id == post.getId()){
                return post;
            }
        }
        throw new BlogPostNotFoundException(String.format("blog post met id %s bestaat niet", id));
    }

    public Blogpost addBlogpost(Blogpost blogpost){
        this.posts.add(blogpost);
        return blogpost;
    }

    public Blogpost addBlogpost(String title, String content){
        return this.addBlogpost(new Blogpost(title, content));
    }

    //update
    public void updateBlogpost(Blogpost blogpost) throws BlogPostNotFoundException{
        Blogpost old = this.getBlogpostById(blogpost.getId());
        old.setTitle(blogpost.getTitle());
        old.setContent(blogpost.getContent());
    }

    //delete
    public void deleteBlogpostById(int id) throws BlogPostNotFoundException{
        this.posts.remove(this.getBlogpostById(id));
        Blogpost.deleteId(id);
    }

}