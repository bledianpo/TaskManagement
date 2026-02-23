import { Link } from "react-router-dom";
import { Flex, Button, Heading, Text, Badge } from "@chakra-ui/react";
import { useAuth } from "../contexts";

const Navbar = () => {
  const { isAuthenticated, user, logout } = useAuth();

  return (
    <Flex
      justify="space-between"
      align="center"
      wrap="wrap"
      gap={3}
      p={{ base: 3, md: 4 }}
      bg="white"
      boxShadow="0 1px 3px rgba(0,0,0,0.1)"
    >
      <Link to="/">
        <Heading size="md" color="#4f46e5" fontWeight="700">
          Task Management
        </Heading>
      </Link>
      <Flex align="center" gap={2} flexWrap="wrap" justifyContent="flex-end">
        {isAuthenticated ? (
          <>
            {user && (
              <>
                <Text fontSize="sm" color="gray.600" maxW={{ base: "120px", sm: "180px" }} title={user.email}>
                  {user.email}
                </Text>
                <Badge colorPalette={user.isAdmin ? "purple" : "gray"} size="sm">
                  {user.isAdmin ? "Admin" : "User"}
                </Badge>
              </>
            )}
            <Link to="/tasks">
              <Button
                size="sm"
                bg="#4f46e5"
                color="white"
                _hover={{ bg: "#4338ca" }}
              >
                {user?.isAdmin ? "All Tasks" : "My Tasks"}
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
      </Flex>
    </Flex>
  );
};

export default Navbar;
