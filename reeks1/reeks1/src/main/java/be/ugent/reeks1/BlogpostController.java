package be.ugent.reeks1;

import be.ugent.Exceptions.BlogPostNotFoundException;
import io.micrometer.core.instrument.MeterRegistry;
import io.micrometer.core.instrument.Counter;

import java.net.URI;
import java.util.ArrayList;


import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.annotation.Secured;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.servlet.support.ServletUriComponentsBuilder;

@RestController
public class BlogpostController {

    private BlogpostDAO blogpostDAO;
    private Counter getCounter;
    private Counter postCounter;
    private Counter putCounter;
    private Counter deleteCounter;


    @Autowired
    public BlogpostController(BlogpostDAO blogpostDAO, MeterRegistry mr){
        this.blogpostDAO = blogpostDAO;
        this.getCounter = mr.counter("get.counter", "operation", "get");
        this.postCounter = mr.counter("post.counter", "operation", "post");
        this.putCounter = mr.counter("put.counter", "operation", "put");
        this.deleteCounter = mr.counter("delete.counter", "operation", "delete");
        
    }

    @GetMapping("/posts")
    public ArrayList<Blogpost> getAllBlogposts(){
        this.getCounter.increment();
        return this.blogpostDAO.getAllBlogposts();
    }

    @GetMapping("/posts/{id}")
    public ResponseEntity<Blogpost> getBlogpostById(@PathVariable int id){
        this.getCounter.increment();
        try{
            Blogpost post = this.blogpostDAO.getBlogpostById(id);

            return new ResponseEntity<>(post, HttpStatus.OK);
        }
        catch(BlogPostNotFoundException e){
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
    }

    //post
    @PostMapping("/posts")
    @Secured({"ADMIN"})
    public ResponseEntity<Blogpost> addBlogpost(@RequestBody Blogpost blogpost){
        this.postCounter.increment();
        Blogpost postAdded = this.blogpostDAO.addBlogpost(blogpost);
        URI location = ServletUriComponentsBuilder.fromCurrentRequest()
                            .path("/{id}")
                            .buildAndExpand(postAdded.getId())
                            .toUri();
        return ResponseEntity.created(location).build();
    }

    //put
    @PutMapping("/posts")
    @Secured({"ADMIN"})
    public ResponseEntity<Blogpost> updateBlogpost(@RequestBody Blogpost blogpost){
        this.putCounter.increment();
        try{
            this.blogpostDAO.updateBlogpost(blogpost);
            return new ResponseEntity<>(HttpStatus.NO_CONTENT);
        }
        catch(BlogPostNotFoundException e){
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);

        }
    }

    //delete
    @DeleteMapping("/posts/{id}")
    @Secured({"ADMIN"})
    public ResponseEntity<Blogpost> deleteBlogpost(@PathVariable int id){
        this.deleteCounter.increment();
        try{
            this.blogpostDAO.deleteBlogpostById(id);
            return new ResponseEntity<>(HttpStatus.NO_CONTENT);
        }
        catch(BlogPostNotFoundException e){
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);

        }
    }

    
}
