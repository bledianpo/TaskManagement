import { useState } from "react";
import {
  Box,
  Button,
  Text,
  Table,
  Flex,
  Badge,
  Spinner,
} from "@chakra-ui/react";
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { useAuth } from "../contexts";
import { GRADIENT_BG } from "../constants";
import type { Task } from "../types/task";
import { deleteTask, getTasks } from "../services/taskService";
import CreateTaskModal from "../components/tasks/CreateTaskModal";
import EditTaskModal from "../components/tasks/EditTaskModal";

const PAGE_SIZE = 10;

const Tasks = function () {
  const queryClient = useQueryClient();
  const [page, setPage] = useState<number>(1);
  const [createOpen, setCreateOpen] = useState<boolean>(false);
  const [editTask, setEditTask] = useState<Task | null>(null);
  const { user } = useAuth();

  const { data, isLoading, error } = useQuery({
    queryKey: ["tasks", user?.userId, page],
    queryFn: () => getTasks({ pageNumber: page, pageSize: PAGE_SIZE }),
    refetchOnWindowFocus: false,
  });

  const deleteMutation = useMutation({
    mutationFn: deleteTask,
    onSuccess: () => queryClient.invalidateQueries({ queryKey: ["tasks"] }),
  });

  if (isLoading) {
    return (
      <Box
        minH="100vh"
        bg={GRADIENT_BG}
        display="flex"
        alignItems="center"
        justifyContent="center"
      >
        <Spinner />
      </Box>
    );
  }

  if (error) {
    return (
      <Box minH="100vh" bg={GRADIENT_BG} px={4} py={6}>
        <Box bg="white" p={6} borderRadius="xl">
          <Text color="red.600">Failed to get Tasks.</Text>
        </Box>
      </Box>
    );
  }

  const { items, totalPages } = data ?? {
    items: [],
    totalPages: 1,
  };

  return (
    <Box minH="100vh" bg={GRADIENT_BG}>
      <Box px={4} py={6} maxW="900px" mx="auto">
        <Box bg="white" borderRadius="1.5rem" p={6} boxShadow="lg">
          <Flex justify="space-between" align="center" mb={6}>
            <Text fontSize="2xl" fontWeight="700" color="gray.800">
              My Tasks
            </Text>
            <Button colorPalette="blue" onClick={() => setCreateOpen(true)}>
              Add task
            </Button>
          </Flex>
          <Table.Root size="sm">
            <Table.Header>
              <Table.Row>
                <Table.ColumnHeader>Title</Table.ColumnHeader>
                <Table.ColumnHeader>Description</Table.ColumnHeader>
                <Table.ColumnHeader>Priority</Table.ColumnHeader>
                <Table.ColumnHeader>Status</Table.ColumnHeader>
                <Table.ColumnHeader textAlign="end">Actions</Table.ColumnHeader>
              </Table.Row>
            </Table.Header>
            <Table.Body>
              {items?.length === 0 ? (
                <Table.Row>
                  <Table.Cell
                    colSpan={5}
                    textAlign="center"
                    py={6}
                    color="gray.500"
                  >
                    No tasks yet. Create one above.
                  </Table.Cell>
                </Table.Row>
              ) : (
                items?.map((task: Task) => (
                  <Table.Row key={task.id}>
                    <Table.Cell fontWeight="medium">{task.title}</Table.Cell>
                    <Table.Cell maxW="200px" truncate title={task.description}>
                      {task.description}
                    </Table.Cell>
                    <Table.Cell>
                      <Badge
                        colorPalette={
                          task.priority === "High"
                            ? "red"
                            : task.priority === "Medium"
                            ? "orange"
                            : "gray"
                        }
                      >
                        {task.priority}
                      </Badge>
                    </Table.Cell>
                    <Table.Cell>
                      <Badge variant="outline">{task.status}</Badge>
                    </Table.Cell>
                    <Table.Cell textAlign="end">
                      <Button
                        size="xs"
                        variant="outline"
                        onClick={() => setEditTask(task)}
                      >
                        Edit
                      </Button>
                      <Button
                        size="xs"
                        colorPalette="red"
                        variant="ghost"
                        ml={2}
                        onClick={() =>
                          confirm("Delete this task?") &&
                          deleteMutation.mutate(task.id)
                        }
                      >
                        Delete
                      </Button>
                    </Table.Cell>
                  </Table.Row>
                ))
              )}
            </Table.Body>
          </Table.Root>
          {totalPages > 1 && (
            <Flex gap={2} mt={4} justify="center" align="center">
              <Button
                size="sm"
                disabled={page <= 1}
                onClick={() => setPage((p) => p - 1)}
              >
                Previous
              </Button>
              <Text fontSize="sm" color="gray.600">
                Page {page} of {totalPages}
              </Text>
              <Button
                size="sm"
                disabled={page >= totalPages}
                onClick={() => setPage((p) => p + 1)}
              >
                Next
              </Button>
            </Flex>
          )}
        </Box>
      </Box>
      <CreateTaskModal
        open={createOpen}
        onClose={() => setCreateOpen(false)}
        onSuccess={() => queryClient.invalidateQueries({ queryKey: ["tasks"] })}
      />
      <EditTaskModal
        open={!!editTask}
        onClose={() => setEditTask(null)}
        task={editTask}
        onSuccess={() => queryClient.invalidateQueries({ queryKey: ["tasks"] })}
      />
    </Box>
  );
};

export default Tasks;
