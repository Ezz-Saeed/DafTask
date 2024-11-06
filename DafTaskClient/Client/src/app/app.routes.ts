import { Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component';
import { HomeComponent } from './Components/home/home.component';
import { authGuard } from './Guards/auth-guard.guard';
import { RegisterComponent } from './Components/register/register.component';
import { UpdateProfileComponent } from './Components/update-profile/update-profile.component';
import { PostsComponent } from './Components/posts/posts.component';
import { AddPostComponent } from './Components/add-post/add-post.component';

export const routes: Routes = [
  {path:'home', component:HomeComponent,canActivate:[authGuard]},
  {path:'updateProfile', component:UpdateProfileComponent,canActivate:[authGuard]},
  {path:'login', component:LoginComponent},
  {path:'deleteProfile', component:LoginComponent,canActivate:[authGuard]},
  {path:'posts', component:PostsComponent,canActivate:[authGuard]},
  {path:'register', component:RegisterComponent},
  {path:'addPost', component:AddPostComponent,canActivate:[authGuard]},
  {path:'',component:LoginComponent},

];
