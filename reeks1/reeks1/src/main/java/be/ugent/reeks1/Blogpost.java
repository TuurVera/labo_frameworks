package be.ugent.reeks1;

import java.util.ArrayList;

import be.ugent.Exceptions.DoubleIdException;

public class Blogpost {
    private static ArrayList<Integer> ids = new ArrayList<>();
    private int id;
    private String title;
    private String content;

    public static void deleteId(int id){
        int index = ids.indexOf(id);
        if(index >= 0){
            ids.remove(index);
        }
    }

    public static int getNewId(){
        boolean idFound = false;
        int id = 0;
        while(!idFound){
            if(ids.indexOf(id) < 0){
                idFound = true;
            }
            else{
                id++;
            }
        }
        return id;
        
    }

    public Blogpost(String title, String content){
        this.id = getNewId();
        ids.add(this.id);
        this.title = title;
        this.content = content;
    }

    public Blogpost(){
        this.id = getNewId();
        ids.add(this.id);
        this.title = "no title";
        this.content = "no content";
    }

    public int getId(){
        return this.id;
    }

    public void setId(int id) throws DoubleIdException{
        if(this.id == id){
            return;
        }
        if(ids.indexOf(id) < 0){
            ids.add(id);
            this.id = id;
        }
        else{
            throw new DoubleIdException(String.format("Id %d al ingenomen", id));
        }
    }

    public String getTitle(){
        return this.title;
    }

    public void setTitle(String title){
        this.title = title;
    }

    public String getContent(){
        return content;
    }

    public void setContent(String content){
        this.content = content;
    }
}
