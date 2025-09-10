import {UserDto} from "../dtos/user-dto";

export interface ValidateUserResponse {
  isValid: boolean;
  user?: UserDto;
}