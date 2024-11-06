import { Component } from '@angular/core';
import { PostsService } from '../../Services/posts.service';
import { IPostDto } from '../../Models/IPostDto';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-add-post',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './add-post.component.html',
  styleUrl: './add-post.component.css'
})
export class AddPostComponent {

  constructor(private postsService:PostsService){}

  addPost(dto:IPostDto){
    dto.datePosted = new Date();
    this.postsService.addPost(dto).subscribe({
      next:response=>{
        console.log(response)
      },
      error:err=>{
        console.log(err)
      }
    })
  }
}
