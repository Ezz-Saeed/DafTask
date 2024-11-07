import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPostDto } from '../Models/IPostDto';

@Injectable({
  providedIn: 'root'
})
export class PostsService {

  baseUrl:string = "http://localhost:5227/api/Posts/";
  constructor(private http:HttpClient) { }

  getPosts(){
    return this.http.get<IPostDto[]>(`${this.baseUrl}getPosts`)
  }

  getSinglePost(id:number){
    return this.http.get<IPostDto>(`${this.baseUrl}getPost?id=${id}`)
  }


  addPost(dto:IPostDto){
    return this.http.post<IPostDto>(`${this.baseUrl}addPost`,dto)
  }

  updatePost(dto:IPostDto,id:number){
    return this.http.put<IPostDto>(`${this.baseUrl}updatePost?id=${id}`,dto)
  }

  deletePost(id:number){
    return this.http.delete<IPostDto>(`${this.baseUrl}deletePost?id=${id}`)
  }
}
