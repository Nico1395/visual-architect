 <script setup lang="ts">
import Icon from '@/components/Icon.vue';
import ButtonGroup from '@/components/ui/button-group/ButtonGroup.vue';
import Button from '@/components/ui/button/Button.vue';
import CodeMirror from 'vue-codemirror6';
import {
  Select,
  SelectContent,
  SelectItem,
  SelectLabel,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select'
import Checkbox from '@/components/ui/checkbox/Checkbox.vue';

const props = defineProps<{
    modelValue: string
    disabled: boolean
    tabSize?: number
}>()
const emits = defineEmits<{
    (e: "update:modelValue", payload: string): void
}>()

</script>

<template>
    <div :class="`design-editor design-code-editor`">
        <div class="design-code-editor-toolbar">
            <ButtonGroup class="design-code-editor-toolbar-language">
                <Select>
                    <SelectTrigger size="sm" class="w-[200px]">
                        <SelectValue placeholder="Select a language" />
                    </SelectTrigger>

                    <SelectContent>
                        <SelectLabel>
                            Languages
                        </SelectLabel>

                        <SelectItem value="csharp">
                            C#
                        </SelectItem>

                        <SelectItem value="js">
                            JavaScript
                        </SelectItem>

                        <SelectItem value="ts">
                            TypeScript
                        </SelectItem>
                    </SelectContent>
                </Select>
            </ButtonGroup>

            <div class="design-code-editor-toolbar-saves">
                <ButtonGroup>
                    <Button size="sm">
                        <Icon icon="ai-save" />

                        Save
                    </Button>

                    <Button size="sm" variant="outline">
                        <Icon icon="ai-arrow-counter-clockwise" />

                        Reset
                    </Button>
                </ButtonGroup>

                <Label for="autosave">
                    <Checkbox id="autosave" :default-value="true" />

                    Autosave
                </Label>
            </div>
        </div>

        <div class="design-code-editor-editor">
            <CodeMirror
                class="code-mirror"
                v-model="props.modelValue"
                basic
                scrollIntoView
                :disabled
                :tabSize="tabSize ?? 4"
            />
        </div>
    </div>
</template>

<style>
.design-code-editor {
    flex: 1;
    display: flex;
    flex-direction: column;
    gap: 0.2rem;
    overflow: hidden;

    .design-code-editor-toolbar {
        display: flex;
        flex-direction: row;
        justify-content: start;
        align-items: center;
        gap: 1rem;

        .design-code-editor-toolbar-saves {
            display: flex;
            flex-direction: row;
            align-items: center;
            gap: 0.5rem;
        }
    }

    .design-code-editor-editor {
        flex: 1;
        overflow: auto;
        border: 1px solid var(--border);
        border-radius: var(--radius-xl);

        .code-mirror {
            height: 100%;

            .cm-editor {
                height: 100%;

                .cm-scroller {
                    font-family: "JetBrains Mono", monospace;
                    font-size: 10pt;
                    font-weight: 500;
                }

                .cm-gutters {
                    color: var(--muted-foreground);
                    background-color: var(--muted-background);
                    border-right: 0;

                }
            }
        }
    }
}
</style>
