import { Component, OnInit } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { UserProfileService } from './Services/user-profile.service';
import { LoginComponent } from "./Components/login/login.component";
import { HomeComponent } from './Components/home/home.component';
import { NavBarComponent } from "./Components/nav-bar/nav-bar.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, LoginComponent, HomeComponent, RouterModule, NavBarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent  implements OnInit{
  title = 'Client';
  constructor(private userProfileService:UserProfileService){}
  ngOnInit(): void {
    // this.userProfileService.login("user@test.com","Ab$$1234").subscribe({
    //     next:response=>{
    //       console.log(response)
    //     },
    //     error:err=>{
    //       console.log(err)
    //     }
    //   });
  }

}
