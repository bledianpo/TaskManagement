import { useState, useEffect } from "react";
import {
  DialogRoot,
  DialogBackdrop,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogBody,
  DialogFooter,
  Button,
  Input,
  Field,
  Text,
} from "@chakra-ui/react";
import { useMutation } from "@tanstack/react-query";
import { FormSelect, PRIORITY_OPTIONS, STATUS_OPTIONS } from "./shared";
import type {
  Task,
  TaskPriority,
  TaskStatus,
  UpdateTaskRequest,
} from "../../types/task";
import { updateTask } from "../../services/taskService";

interface EditTaskModalProps {
  open: boolean;
  onClose: () => void;
  task: Task | null;
  onSuccess: () => void;
}

const EditTaskModal = function ({
  open,
  onClose,
  task,
  onSuccess,
}: EditTaskModalProps) {
  const [title, setTitle] = useState<string>("");
  const [description, setDescription] = useState<string>("");
  const [priority, setPriority] = useState<TaskPriority>("Low");
  const [status, setStatus] = useState<TaskStatus>("Draft");
  const [error, setError] = useState<string>("");

  useEffect(() => {
    if (!task) return;

    setTitle(task.title);
    setDescription(task.description);
    setPriority(task.priority);
    setStatus(task.status);
    setError("");
  }, [task]);

  const updateMutation = useMutation({
    mutationFn: (payload: UpdateTaskRequest) => updateTask(task!.id, payload),
    onSuccess: () => {
      onSuccess();
      handleClose();
    },
    onError: (err) => {
      setError(err?.message || "Failed to update task.");
    },
  });

  const handleClose = () => {
    setError("");
    onClose();
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    if (!task) return;

    const trimmedTitle = title.trim();
    const trimmedDescription = description.trim();

    if (!trimmedTitle) {
      setError("Title is required");
      return;
    }

    if (!trimmedDescription) {
      setError("Description is required");
      return;
    }

    setError("");

    updateMutation.mutate({
      title: trimmedTitle,
      description: trimmedDescription,
      priority,
      status,
    });
  };

  if (!task) return null;

  return (
    <DialogRoot open={open} onOpenChange={(e) => !e.open && handleClose()}>
      <DialogBackdrop />
      <DialogContent
        position="fixed"
        top="50%"
        left="50%"
        transform="translate(-50%, -50%)"
        margin="0"
        maxH="90vh"
        overflowY="auto"
      >
        <DialogHeader>
          <DialogTitle>Edit task</DialogTitle>
        </DialogHeader>

        <DialogBody>
          <form id="edit-task-form" onSubmit={handleSubmit}>
            <Field.Root>
              <Field.Label>Title</Field.Label>
              <Input value={title} onChange={(e) => setTitle(e.target.value)} />
            </Field.Root>

            <Field.Root mt={3}>
              <Field.Label>Description</Field.Label>
              <Input
                value={description}
                onChange={(e) => setDescription(e.target.value)}
              />
            </Field.Root>

            <FormSelect
              label="Priority"
              value={priority}
              options={PRIORITY_OPTIONS}
              onChange={(v) => setPriority(v as TaskPriority)}
            />

            <FormSelect
              label="Status"
              value={status}
              options={STATUS_OPTIONS}
              onChange={(v) => setStatus(v as TaskStatus)}
            />
          </form>

          {error && (
            <Text color="red.500" fontSize="sm" mt={2}>
              {error}
            </Text>
          )}
        </DialogBody>

        <DialogFooter>
          <Button variant="outline" onClick={handleClose}>
            Cancel
          </Button>

          <Button
            type="submit"
            form="edit-task-form"
            colorPalette="blue"
            loading={updateMutation.isPending}
          >
            Save
          </Button>
        </DialogFooter>
      </DialogContent>
    </DialogRoot>
  );
};

export default EditTaskModal;
