import type { LoginRequest, LoginResponse, RegisterRequest, RegisterResponse } from "../types/auth";
import { api } from "./api";

export const login = async (data: LoginRequest) => {
  const response = await api.post<LoginResponse>("/auth/login", data);
  return response.data;
};

export const register = async (data: RegisterRequest) => {
  const response = await api.post<RegisterResponse>("/auth/register", data);
  return response.data;
};
