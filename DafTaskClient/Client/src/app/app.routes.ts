import { Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component';
import { HomeComponent } from './Components/home/home.component';
import { authGuard } from './Guards/auth-guard.guard';
import { RegisterComponent } from './Components/register/register.component';
import { UpdateProfileComponent } from './Components/update-profile/update-profile.component';

export const routes: Routes = [
  {path:'home', component:HomeComponent,canActivate:[authGuard]},
  {path:'updateProfile', component:UpdateProfileComponent,canActivate:[authGuard]},
  {path:'login', component:LoginComponent},
  {path:'deleteProfile', component:LoginComponent},
  {path:'register', component:RegisterComponent},
  {path:'',component:LoginComponent},

];
