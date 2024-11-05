import { Component } from '@angular/core';
import { UserProfileService } from '../../Services/user-profile.service';
import { IRegisterDto } from '../../Models/IRegisterDto';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {

  constructor(private userProfileService:UserProfileService){}

  register(dto:IRegisterDto){
    this.userProfileService.register(dto).subscribe({
      next:response=>{
        sessionStorage.setItem('token', response.data.token);
        console.log(response)
      },
      error:err=>{
        console.log(err);
      }
    })
  }
}
