import { Box, Button, Text } from "@chakra-ui/react";
import { Link } from "react-router-dom";
import { useAuth } from "../contexts";
import { GRADIENT_BG } from "../constants";

const Tasks = function () {
  const { user, logout } = useAuth();
  return (
    <Box minH="100vh" bg={GRADIENT_BG}>
      <Box px={4} py={6} maxW="900px" mx="auto">
        <Box
          display="flex"
          justifyContent="space-between"
          alignItems="center"
          mb={8}
        >
          <Text fontSize="2xl" fontWeight="700" color="white">
            My Tasks
          </Text>
          <Box display="flex" gap={2}>
            <Link to="/">
              <Button variant="ghost" size="sm" color="white">
                Home
              </Button>
            </Link>
            <Button
              onClick={logout}
              variant="outline"
              size="sm"
              borderColor="white"
              color="white"
            >
              Log out
            </Button>
          </Box>
        </Box>
        <Box
          bg="white"
          borderRadius="1.5rem"
          p={8}
          boxShadow="0 25px 50px -12px rgba(0,0,0,0.15)"
        >
          <Text color="#4a5568">
            {user ? `Logged in as ${user.email}` : ""}
          </Text>
        </Box>
      </Box>
    </Box>
  );
};

export default Tasks;
