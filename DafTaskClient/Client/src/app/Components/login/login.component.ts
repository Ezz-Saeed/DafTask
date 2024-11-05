import { Component, OnInit } from '@angular/core';
import { UserProfileService } from '../../Services/user-profile.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IUserProfile } from '../../Models/IUserProfile';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent  {

  constructor(private userProfileService:UserProfileService,private http:HttpClient){}

  login(email:string,password:string){
    this.userProfileService.login(email,password).subscribe({
      next:response=>{
        console.log(response)
        if(response.data.token){
         localStorage.setItem('token', response.data.token)
        }

      },
      error:err=>{
        console.log(err)
      }
    });
  }



}
