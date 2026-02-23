import { useMemo } from "react";
import { createListCollection, Field, Flex, Select } from "@chakra-ui/react";

export const FormSelect = function ({
  label,
  value,
  options,
  onChange,
  inline,
}: {
  label: string;
  value: string;
  options: { value: string; label: string }[];
  onChange: (value: string) => void;
  inline?: boolean;
}) {
  const collection = useMemo(
    () =>
      createListCollection<{ value: string; label: string }>({
        items: options,
        itemToString: (item) => item.label,
        itemToValue: (item) => item.value,
      }),
    [options]
  );

  const selectContent = (
    <Select.Root
      collection={collection}
      value={value ? [value] : []}
      onValueChange={(e) => onChange(e.value[0] ?? "")}
    >
      <Select.HiddenSelect />
      <Select.Control minW={inline ? "120px" : undefined}>
        <Select.Trigger>
          <Select.ValueText placeholder="Select..." />
        </Select.Trigger>
        <Select.IndicatorGroup>
          <Select.Indicator />
        </Select.IndicatorGroup>
      </Select.Control>
      <Select.Positioner>
        <Select.Content>
          {options.map((o) => (
            <Select.Item key={o.value} item={o}>
              <Select.ItemText>{o.label}</Select.ItemText>
            </Select.Item>
          ))}
        </Select.Content>
      </Select.Positioner>
    </Select.Root>
  );

  return (
    <Field.Root mt={inline ? 0 : 3}>
      {inline ? (
        <Flex align="center" gap={2} flexShrink={0}>
          <Field.Label mb={0} flexShrink={0} fontSize="sm" color="gray.600">
            {label}
          </Field.Label>
          {selectContent}
        </Flex>
      ) : (
        <>
          <Field.Label>{label}</Field.Label>
          {selectContent}
        </>
      )}
    </Field.Root>
  );
};

export const PRIORITY_OPTIONS = [
  { value: "Low", label: "Low" },
  { value: "Medium", label: "Medium" },
  { value: "High", label: "High" },
];

export const STATUS_OPTIONS = [
  { value: "Draft", label: "Draft" },
  { value: "InProgress", label: "In Progress" },
  { value: "Completed", label: "Completed" },
];
