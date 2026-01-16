<script setup lang="ts">
import Button from './ui/button/Button.vue';
import VueMarkdown from 'vue-markdown-render'
import Textarea from './ui/textarea/Textarea.vue';
import { nextTick, ref, watch } from 'vue';
import { useI18n } from 'vue-i18n';
import Icon from './Icon.vue';
import ButtonGroup from './ui/button-group/ButtonGroup.vue';

// TODO -> Actions such as bold or italic arent recognized as an input and cannot be undone using undo

const { t } = useI18n();
const props = defineProps<{
    modelValue: string,
    editing?: boolean,
    disabled?: boolean,
    id?: string,
    class?: string,
    placeholder?: string | null,
}>()
const emits = defineEmits<{
    (e: "update:modelValue", payload: string): void
    (e: "save"): void
}>()

const isEditing = ref(props.editing ?? false)
const draft = ref(props.modelValue)
const textareaRef = ref<{ el: HTMLTextAreaElement | null } | null>(null)

function startEditing() {
    if (props.disabled)
        return

    draft.value = props.modelValue
    isEditing.value = true

    nextTick(() => {
        const area = textareaRef.value?.el
        if (!area)
            return

        area.scrollTop = 0
        area.focus({ preventScroll: true })
    })
}

function save() {
    if (props.disabled || !isEditing.value)
        return

    emits('update:modelValue', draft.value)
    emits('save')
    isEditing.value = false
}

function cancel() {
    if (props.disabled || !isEditing.value)
        return

    draft.value = props.modelValue
    isEditing.value = false
}

function withSelection(transform: (selected: string) => string, cursorOffset?: number) {
    const area = textareaRef.value?.el
    if (!area)
        return

    // Save the current scroll position to prevent scrolling later
    const scrollTop = area.scrollTop

    const start = area.selectionStart
    const end = area.selectionEnd
    const value = draft.value

    // Remove leading and trailing whitespace for better UX
    const selected = value.slice(start, end)
    const leadingSpaces = selected.match(/^\s*/)?.[0]?.length ?? 0
    const trailingSpaces = selected.match(/\s*$/)?.[0]?.length ?? 0
    const trimmed = selected.trim()

    const replacement = transform(trimmed)

    draft.value =
        value.slice(0, start + leadingSpaces) +
        replacement +
        value.slice(end - trailingSpaces, value.length)

    nextTick(() => {
        area.scrollTop = scrollTop

        const pos = cursorOffset !== undefined
            ? start + leadingSpaces + cursorOffset
            : start + leadingSpaces + replacement.length

        area.setSelectionRange(pos, pos)
        area.focus({ preventScroll: true })
    })
}

function bold() {
    withSelection(text =>
        text.startsWith('**') && text.endsWith('**')
            ? text.slice(2, -2)
            : `**${text || ''}**`,
        2
    )
}

function italic() {
    withSelection(text =>
        text.startsWith('*') && text.endsWith('*')
            ? text.slice(1, -1)
            : `*${text || ''}*`,
        1
    )
}

function strike() {
    withSelection(text =>
        text.startsWith('~~') && text.endsWith('~~')
            ? text.slice(2, -2)
            : `~~${text || ''}~~`,
        2
    )
}

function header(level: 1 | 2 | 3) {
    withSelection(text => {
        const trimmed = text.replace(/^#{1,6}\s*/, "")
        return `${"#".repeat(level)} ${trimmed}`
    })
}

function triggerUndo() {
    const area = textareaRef.value?.el
    if (!area)
        return

    area.focus({ preventScroll: true })
    document.execCommand('undo')
}

function triggerRedo() {
    const area = textareaRef.value?.el
    if (!area)
        return

    area.focus({ preventScroll: true })
    document.execCommand('redo')
}

watch(
    () => props.modelValue,
    (v) => {
        if (!isEditing.value) {
            draft.value = v
        }
    }
)
</script>

<template>
    <div :class="`markdown-area ${isEditing ? 'editing' : 'viewing'} ${props.class ?? ''}`.trim()">
        <div class="markdown-area-header" v-if="isEditing">
            <div class="markdown-area-header-actions">
                <ButtonGroup>
                    <Button size="icon-sm" variant="default" :disabled type="button" @click="save">
                        <Icon icon="ai-save" />
                    </Button>

                    <Button size="icon-sm" variant="outline" :disabled type="button" @click="cancel">
                        <Icon icon="ai-cross" />
                    </Button>
                </ButtonGroup>

                <ButtonGroup>
                    <Button size="icon-sm" variant="outline" @click="triggerUndo">
                        <Icon icon="ai-arrow-counter-clockwise" />
                    </Button>

                    <Button size="icon-sm" variant="outline" @click="triggerRedo">
                        <Icon icon="ai-arrow-clockwise" />
                    </Button>
                </ButtonGroup>

                <ButtonGroup>
                    <Button size="icon-sm" variant="outline" @click="() => header(1)">
                        <Icon icon="bi bi-type-h1" />
                    </Button>

                    <Button size="icon-sm" variant="outline" @click="() => header(2)">
                        <Icon icon="bi bi-type-h2" />
                    </Button>

                    <Button size="icon-sm" variant="outline" @click="() => header(3)">
                        <Icon icon="bi bi-type-h3" />
                    </Button>
                </ButtonGroup>

                <ButtonGroup>
                    <Button size="icon-sm" variant="outline" @click="bold">
                        <Icon icon="bi bi-type-bold" />
                    </Button>

                    <Button size="icon-sm" variant="outline" @click="italic">
                        <Icon icon="bi bi-type-italic" />
                    </Button>

                    <Button size="icon-sm" variant="outline" @click="strike">
                        <Icon icon="bi bi-type-strikethrough" />
                    </Button>
                </ButtonGroup>
            </div>

            <div class="markdown-area-header-title">
                <slot name="title" />
            </div>
        </div>

        <div v-if="isEditing" class="markdown-area-body">
            <Textarea
                ref="textareaRef"
                :disabled
                :id
                v-model="draft"
            />
        </div>

        <div v-else class="markdown-area-body rendered">
            <div class="markdown-area-body-actions-wrapper">
                <ButtonGroup class="markdown-area-body-actions">
                    <Button size="sm" variant="outline" @click="startEditing()">
                        <Icon icon="ai-pencil" />
                        {{ t('actions.edit') }}
                    </Button>
                </ButtonGroup>
            </div>

            <VueMarkdown :disabled class="markdown" :source="modelValue" v-if="modelValue" />

            <div class="markdown-area-body-placeholder" v-else>
                {{ props.placeholder ?? t('components.markdownArea.placeholder') }}
            </div>
        </div>
    </div>
</template>

<style>
.markdown-area {
    display: flex;
    flex-direction: column;
    border-radius: var(--radius-md);
    border: 1px solid var(--border);
    min-height: 300px;

    &.viewing {
        cursor: text;
    }

    .markdown-area-header {
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        align-items: center;
        padding: 0.3rem;
        border-radius: var(--radius-xl) var(--radius-xl) 0 0;
        border-bottom: 1px solid var(--border);
        position: sticky;
        top: 0;
        z-index: 10;
        background-color: var(--background);

        .markdown-area-header-title {
            flex: none;
            font-size: 10pt;
            font-weight: 500;
            color: var(--muted-foreground);
        }

        .markdown-area-header-actions {
            flex: none;
            display: flex;
            flex-direction: row;
            gap: 0.3rem;
        }
    }

    .markdown-area-body {
        flex: 1;
        display: flex;
        flex-direction: column;
        position: relative;

        > * {
            height: 100%;
        }

        > textarea {
            flex: 1;
            font-family: "JetBrains Mono";
            font-weight: 500;
            font-size: 10pt;
            resize: none;
            padding: 0.5rem;
            overflow-y: auto;
            border-radius: 0 0 var(--radius-xl) var(--radius-xl);
            border: none;
            background-color: var(--muted-background);

            &::placeholder {
                font-weight: 400;
            }
        }

        > .markdown {
            font-size: 10pt;
            padding: 0.5rem 0.7rem;
            overflow-y: auto;
        }

        .markdown-area-body-actions-wrapper {
            display: none;
            position: absolute;
            top: 0;
            right: 0;
            width: 100%;
            pointer-events: none;
            padding: 0.5rem;

            .markdown-area-body-actions {
                position: sticky;
                top: 0.5rem;
                float: right;
                z-index: 10;
                background-color: var(--background);
                pointer-events: auto;
                display: inline-flex;
            }
        }

        &:hover {
            .markdown-area-body-actions-wrapper {
                display: unset;
            }
        }

        .markdown-area-body-placeholder {
            font-weight: 500;
            font-size: 10pt;
            color: var(--muted-foreground);
            text-align: center;
            margin: 1rem auto;
        }
    }
}

.markdown {
  line-height: 1.7;
  color: inherit;
}

/* Headings */
.markdown h1 {
  font-size: 1.7rem;
  font-weight: 700;
  margin: 0.7rem 0;
}

.markdown h2 {
  font-size: 1.35rem;
  font-weight: 600;
  margin: 0.55rem 0 0.55rem;
}

.markdown h3 {
  font-size: 1rem;
  font-weight: 600;
  margin: 0.3rem 0 0.3rem;
}

.markdown p {
  margin: 0.65rem 0;
}

.markdown ul,
.markdown ol {
  padding-left: 1.5rem;
  margin: 0.75rem 0;
}

.markdown ul {
  list-style: disc;
}

.markdown ol {
  list-style: decimal;
}

.markdown code {
  font-family: "JetBrains Mono", monospace;
  background-color: var(--muted-background);
  padding: 0.15rem 0.35rem;
  border-radius: 4px;
}

.markdown pre {
  background-color: var(--muted-background);
  padding: 1rem;
  border-radius: 6px;
  overflow-x: auto;
}

.markdown pre code {
  background: none;
  padding: 0;
}

.markdown a {
    color: var(--link);
    text-decoration: 1px underline currentColor;

    &:visited {
        color: var(--visited-link);
    }
}
</style>
