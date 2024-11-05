import { Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component';
import { HomeComponent } from './Components/home/home.component';
import { authGuard } from './Guards/auth-guard.guard';

export const routes: Routes = [
  {path:'home', component:HomeComponent,canActivate:[authGuard]},
  {path:'login', component:LoginComponent},
  {path:'',component:LoginComponent},

];
