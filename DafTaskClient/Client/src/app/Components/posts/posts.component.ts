import { Component, OnInit } from '@angular/core';
import { PostsService } from '../../Services/posts.service';
import { IPostDto } from '../../Models/IPostDto';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-posts',
  standalone: true,
  imports: [CommonModule,RouterModule],
  templateUrl: './posts.component.html',
  styleUrl: './posts.component.css'
})
export class PostsComponent implements OnInit {
  posts?:IPostDto[];
  constructor(private postsService:PostsService){}
  ngOnInit(): void {
    this.getPosts();
  }

  private getPosts(){
    this.postsService.getPosts().subscribe({
      next:response=>{
        this.posts=response
        // console.log(response)
      },
      error:err=>{
        console.log(err)
      }
    })
  }

  // addPost(dto:IPostDto){
  //   this.postsService.addPost(dto).subscribe({
  //     next:response=>{
  //       console.log(response)
  //     },
  //     error:err=>{
  //       console.log(err)
  //     }
  //   })
  // }

}
