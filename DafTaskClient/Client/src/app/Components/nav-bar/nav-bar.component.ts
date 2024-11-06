import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { UserProfileService } from '../../Services/user-profile.service';

@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css'
})
export class NavBarComponent {

  constructor(private userProfileService:UserProfileService,private router:Router){}

  logout(){
    this.userProfileService.logout();
  }

  deleteProfile(){

    this.userProfileService.deleteProfile().subscribe({
      next:response=>{
        console.log(response);
        // this.router.navigate([`/login`])
      },
      error:err=>{
        console.log(err);
      }
    })
    sessionStorage.removeItem('token')
  }

}
