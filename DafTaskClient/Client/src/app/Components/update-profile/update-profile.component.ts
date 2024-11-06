import { Component } from '@angular/core';
import { UserProfileService } from '../../Services/user-profile.service';
import { UpdateProfileDto } from '../../Models/UpdateProfileDto';
import { Router } from '@angular/router';
import { FormsModule, FormGroup,ReactiveFormsModule, FormBuilder} from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-update-profile',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule,],
  templateUrl: './update-profile.component.html',
  styleUrl: './update-profile.component.css'
})
export class UpdateProfileComponent {

  updateForm!:FormGroup
  constructor(private userProfileService:UserProfileService,private router:Router,
    private fb:FormBuilder
  ){

    this.updateForm = fb.group({
      email:[],
      password:[],
      oldPassword:[],
      dateOfBirth:[],
      lastName:[],
      firstName:[],
      newEmail:[],
    });
  }

  updateProfile(){
    var dto = this.updateForm.value as UpdateProfileDto
    dto.dateOfBirth = dto.dateOfBirth ?? new Date();
    console.log(dto)
    this.userProfileService.updateProfile(dto).subscribe({
      next:response=>{
        this.userProfileService.logout();
        // sessionStorage.setItem('token',response.data.token)
        this.router.navigate(['/login'])
        // console.log(response);
      },
      error:err=>{
        console.log(err)
      }
    })
  }

}
