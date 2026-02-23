import axios from "axios";

const defaultBaseURL = "https://localhost:7225/api";

export const api = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL ?? defaultBaseURL,
});

const TOKEN_KEY = "token";
const USER_KEY = "user";

export const AUTH_SESSION_EXPIRED_EVENT = "auth:sessionExpired";

export const getApiErrorMessage = function (
  error: unknown,
  fallback: string
): string {
  if (error && typeof error === "object" && "response" in error) {
    const res = (error as { response?: { data?: { message?: string } } }).response;
    const msg = res?.data?.message;
    if (typeof msg === "string" && msg.trim()) {
      return msg;
    }
  }
  if (error instanceof Error && error.message) {
    return error.message;
  }
  return fallback;
};

api.interceptors.request.use((config) => {
  const token = localStorage.getItem(TOKEN_KEY);
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      const url = error.config?.url ?? "";
      const isLoginOrRegister =
        url.includes("/auth/login") || url.includes("/auth/register");
      if (!isLoginOrRegister) {
        localStorage.removeItem(TOKEN_KEY);
        localStorage.removeItem(USER_KEY);
        window.dispatchEvent(new CustomEvent(AUTH_SESSION_EXPIRED_EVENT));
      }
    }
    return Promise.reject(error);
  }
);
