export class UpdateProfileDto {
  email!: string;
  newEmail?: string;
  password?: string;
  oldPassword?: string;
  firstName?: string;
  lastName?: string;
  dateOfBirth = Date.now;
}