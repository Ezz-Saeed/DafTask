
export interface IUserProfile{
  statusCode: number;
  message:string;
  data:IAuthenticatione;
}

export interface IAuthenticatione {
  email: string;
  token: string;
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  userName: string;
}