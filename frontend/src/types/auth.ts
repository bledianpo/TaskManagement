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

export type User = { userId: number; email: string; isAdmin: boolean } | null;

export interface AuthContextValue {
  token: string | null;
  user: User;
  isLoading: boolean;
  login: (email: string, password: string) => Promise<void>;
  register: (username: string, email: string, password: string) => Promise<void>;
  logout: () => void;
  isAuthenticated: boolean;
}

