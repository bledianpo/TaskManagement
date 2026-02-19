import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { Box, Button, Text, Input, Field, VStack } from "@chakra-ui/react";
import { useAuth } from "../contexts";
import { GRADIENT_BG } from "../constants";

const MIN_PASSWORD_LENGTH = 6;

const Register = () => {
  const [username, setUsername] = useState<string>("");
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [error, setError] = useState<string>("");
  const [loading, setLoading] = useState<boolean>(false);
  const { register } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");
    if (!username.trim()) {
      setError("Username is required");
      return;
    }
    if (username.trim().length < 2) {
      setError("Username must be at least 2 characters");
      return;
    }
    if (!email.trim()) {
      setError("Email is required");
      return;
    }
    if (!password) {
      setError("Password is required");
      return;
    }
    if (password.length < MIN_PASSWORD_LENGTH) {
      setError(`Password must be at least ${MIN_PASSWORD_LENGTH} characters`);
      return;
    }
    setLoading(true);
    try {
      await register(username.trim(), email.trim().toLowerCase(), password);
      navigate("/tasks", { replace: true });
    } catch (err) {
      setError(err instanceof Error ? err.message : "Registration failed");
    } finally {
      setLoading(false);
    }
  };

  return (
    <Box
      as="main"
      minH="100vh"
      display="flex"
      alignItems="center"
      justifyContent="center"
      p={4}
      bg={GRADIENT_BG}
    >
      <Box
        w="100%"
        maxW="400px"
        bg="rgba(255,255,255,0.98)"
        borderRadius="1.5rem"
        boxShadow="0 25px 50px -12px rgba(0,0,0,0.25)"
        p={8}
      >
        <Text fontSize="2xl" fontWeight="700" color="#1a202c" mb={2}>
          Create account
        </Text>
        <Text color="#718096" fontSize="sm" mb={6}>
          Register to manage your tasks
        </Text>
        <form onSubmit={handleSubmit}>
          <VStack gap={4} align="stretch">
            <Field.Root>
              <Field.Label color="#1a202c" fontWeight="500">Username</Field.Label>
              <Input
                type="text"
                placeholder="Your name"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
                autoComplete="username"
                borderColor="#e2e8f0"
                color="#1a202c"
              />
            </Field.Root>
            <Field.Root>
              <Field.Label color="#1a202c" fontWeight="500">Email</Field.Label>
              <Input
                type="email"
                placeholder="you@example.com"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                autoComplete="email"
                borderColor="#e2e8f0"
                color="#1a202c"
              />
            </Field.Root>
            <Field.Root>
              <Field.Label color="#1a202c" fontWeight="500">Password</Field.Label>
              <Input
                type="password"
                placeholder="At least 6 characters"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                autoComplete="new-password"
                borderColor="#e2e8f0"
                color="#1a202c"
              />
            </Field.Root>
            {error && (
              <Text color="red.500" fontSize="sm">
                {error}
              </Text>
            )}
            <Button
              type="submit"
              w="full"
              bg="#4f46e5"
              color="white"
              _hover={{ bg: "#4338ca" }}
              loading={loading}
              disabled={loading}
            >
              Register
            </Button>
          </VStack>
        </form>
        <Text mt={4} fontSize="sm" color="#718096" textAlign="center">
          Already have an account?{" "}
          <Link to="/login" style={{ color: "#4f46e5", fontWeight: 600 }}>
            Log in
          </Link>
        </Text>
        <Link to="/">
          <Button variant="ghost" size="sm" color="#718096" mt={2} w="full">
          Back to home
          </Button>
        </Link>
      </Box>
    </Box>
  );
};

export default Register;
