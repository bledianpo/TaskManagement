import { Link } from "react-router-dom";
import { Box, Flex, Button, Heading } from "@chakra-ui/react";
import { useAuth } from "../contexts";

const Navbar = () => {
  const { isAuthenticated, logout } = useAuth();

  return (
    <Flex
      justify="space-between"
      align="center"
      p={4}
      bg="white"
      boxShadow="0 1px 3px rgba(0,0,0,0.1)"
    >
      <Link to="/">
        <Heading size="md" color="#4f46e5" fontWeight="700">
          Task Management
        </Heading>
      </Link>
      <Box display="flex" gap={2}>
        {isAuthenticated ? (
          <>
            <Link to="/tasks">
              <Button
                size="sm"
                bg="#4f46e5"
                color="white"
                _hover={{ bg: "#4338ca" }}
              >
                My Tasks
              </Button>
            </Link>
            <Button
              size="sm"
              variant="outline"
              borderColor="#4f46e5"
              color="#4f46e5"
              onClick={logout}
            >
              Log out
            </Button>
          </>
        ) : (
          <>
            <Link to="/login">
              <Button
                size="sm"
                bg="#4f46e5"
                color="white"
                _hover={{ bg: "#4338ca" }}
              >
                Log in
              </Button>
            </Link>
            <Link to="/register">
              <Button
                size="sm"
                variant="outline"
                borderColor="#4f46e5"
                color="#4f46e5"
              >
                Register
              </Button>
            </Link>
          </>
        )}
      </Box>
    </Flex>
  );
};

export default Navbar;
