import { Routes, Route, Navigate } from "react-router-dom";
import { Box, Spinner } from "@chakra-ui/react";
import { useAuth } from "./contexts";
import Navbar from "./components/Navbar";
import Home from "./pages/Home";
import Login from "./pages/Login";
import Register from "./pages/Register";
import Tasks from "./pages/Tasks";

const ProtectedRoute = ({ children }: { children: React.ReactNode }) => {
  const { isAuthenticated, isLoading } = useAuth();
  if (isLoading) {
    return (
      <Box
        minH="100vh"
        display="flex"
        alignItems="center"
        justifyContent="center"
        bg="#4f46e5"
      >
        <Spinner size="xl" color="white" />
      </Box>
    );
  }
  if (!isAuthenticated) {
    return <Navigate to="/login" replace />;
  }
  return <>{children}</>;
};

const GuestRoute = ({ children }: { children: React.ReactNode }) => {
  const { isAuthenticated, isLoading } = useAuth();
  if (isLoading) {
    return (
      <Box
        minH="100vh"
        display="flex"
        alignItems="center"
        justifyContent="center"
        bg="#4f46e5"
      >
        <Spinner size="xl" color="white" />
      </Box>
    );
  }
  if (isAuthenticated) {
    return <Navigate to="/tasks" replace />;
  }
  return <>{children}</>;
};

const AppRouter = () => (
  <Box minH="100vh" w="100%">
    <Navbar />
    <Routes>
      <Route path="/" element={<Home />} />
      <Route
        path="/login"
        element={
          <GuestRoute>
            <Login />
          </GuestRoute>
        }
      />
      <Route
        path="/register"
        element={
          <GuestRoute>
            <Register />
          </GuestRoute>
        }
      />
      <Route
        path="/tasks"
        element={
          <ProtectedRoute>
            <Tasks />
          </ProtectedRoute>
        }
      />
      <Route path="*" element={<Navigate to="/" replace />} />
    </Routes>
  </Box>
);

export default AppRouter;
