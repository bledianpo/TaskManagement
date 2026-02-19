export interface LoginRequest {
    email: string;
    password: string;
  }

export interface LoginResponse {
  token: string;
  userId: number;
  email: string;
  isAdmin: boolean;
}

export interface RegisterRequest {
  username: string;
  email: string;
  password: string;
}

export interface RegisterResponse {
  message: string;
}