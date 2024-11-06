import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IUserProfile } from '../Models/IUserProfile';
import { Observable } from 'rxjs';
import { IRegisterDto } from '../Models/IRegisterDto';
import { UpdateProfileDto } from '../Models/UpdateProfileDto';

@Injectable({
  providedIn: 'root'
})
export class UserProfileService {

  baseUrl:string = "http://localhost:5227/api/UserProfile/";
  constructor(private http:HttpClient) { }

  login(email:string,password:string):Observable<IUserProfile>{
    return this.http.post<IUserProfile>(`${this.baseUrl}login`,
    {email:email,password:password})
  }


  register(dto:IRegisterDto){
    return this.http.post<IUserProfile>(`${this.baseUrl}register`,dto)
  }

  updateProfile(dto:UpdateProfileDto){
    return this.http.put<IUserProfile>(`${this.baseUrl}update`,dto)
  }

  deleteProfile(){
    return this.http.delete<string>(`${this.baseUrl}delete`)
  }

  logout() {
    sessionStorage.removeItem('token');
  }

  getToken() {
    return sessionStorage.getItem('token');
  }

  isAuthenticated(): boolean {
    // Basic check if the token exists
    return !!this.getToken();
  }

}
