import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UserProfileService } from '../../Services/user-profile.service';

@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css'
})
export class NavBarComponent {

  constructor(private userProfileService:UserProfileService){}

  logout(){
    this.userProfileService.logout();
  }

}
