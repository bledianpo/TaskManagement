import {
  createContext,
  useCallback,
  useContext,
  useEffect,
  useState,
} from "react";
import type { User, AuthContextValue } from "../types/auth";
import {
  login as authServiceLogin,
  register as authServiceRegister,
} from "../services/authService";
import { useNavigate } from "react-router-dom";

const AuthContext = createContext<AuthContextValue | null>(null);

const TOKEN_KEY = "token";
const USER_KEY = "user";

export const AuthProvider = ({ children }: { children: React.ReactNode }) => {
  const navigate = useNavigate();
  const [token, setToken] = useState<string | null>(null);
  const [user, setUser] = useState<User | null>(null);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    try {
      const t = localStorage.getItem(TOKEN_KEY);
      const u = localStorage.getItem(USER_KEY);
      setToken(t);
      setUser(u ? (JSON.parse(u) as User) : null);
    } catch {
      setToken(null);
      setUser(null);
    } finally {
      setIsLoading(false);
    }
  }, []);

  const login = useCallback(async (email: string, password: string) => {
    try {
      const data = await authServiceLogin({
        email: email.trim(),
        password,
      });

      const userData: User = {
        userId: data.userId,
        email: data.email,
        isAdmin: data.isAdmin,
      };

      localStorage.setItem(TOKEN_KEY, data.token);
      localStorage.setItem(USER_KEY, JSON.stringify(userData));

      setToken(data.token);
      setUser(userData);
      alert("Login successful!");
    } catch (error: any) {
      setToken(null);
      setUser(null);
      alert(error.message || "Login failed");
    }
  }, []);

  const register = useCallback(
    async (username: string, email: string, password: string) => {
      try {
        await authServiceRegister({
          username: username.trim(),
          email: email.trim(),
          password,
        });

        alert("Registration successful! Please login.");
        navigate("/login", { replace: true });
      } catch (error: any) {
        alert(error.message || "Registration failed");
      }
    },
    [navigate]
  );

  const logout = useCallback(() => {
    localStorage.removeItem(TOKEN_KEY);
    localStorage.removeItem(USER_KEY);
    setToken(null);
    setUser(null);
    alert("Logged out");
    navigate("/login", { replace: true });
  }, [navigate]);

  const value: AuthContextValue = {
    token,
    user,
    isLoading,
    login,
    register,
    logout,
    isAuthenticated: !!token,
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

export const useAuth = (): AuthContextValue => {
  const ctx = useContext(AuthContext);
  if (!ctx) throw new Error("useAuth must be used within AuthProvider");
  return ctx;
};
