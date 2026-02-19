import { Box, Text } from "@chakra-ui/react";
import { useAuth } from "../contexts";
import { GRADIENT_BG } from "../constants";

const Tasks = function () {
  const { user } = useAuth();
  return (
    <Box minH="100vh" bg={GRADIENT_BG}>
      <Box px={4} py={6} maxW="900px" mx="auto">
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
