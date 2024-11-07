import { Component, Input, OnInit } from '@angular/core';
import { PostsService } from '../../Services/posts.service';
import { IPostDto } from '../../Models/IPostDto';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-edit-post',
  standalone: true,
  imports: [],
  templateUrl: './edit-post.component.html',
  styleUrl: './edit-post.component.css'
})
export class EditPostComponent implements OnInit {

 postId!:number;
  post!:IPostDto
  constructor(private postsService:PostsService,private activatedRoute:ActivatedRoute){}
  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe(paramMap=>{
      this.postId = Number(paramMap.get("id"));
    })
    this.postsService.getSinglePost(this.postId).subscribe({
      next:response=>{
        this.post = response;
      }
    })
  }

  updatePost(dto:IPostDto){
    this.postsService.updatePost(dto,this.postId).subscribe({
      next:response=>{
        console.log(response)
      },
      error:err=>{
        console.log(err)
      }
    })
  }
}
