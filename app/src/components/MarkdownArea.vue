<script setup lang="ts">
import Button from './ui/button/Button.vue';
import VueMarkdown from 'vue-markdown-render'
import { nextTick, ref, watch } from 'vue';
import { useI18n } from 'vue-i18n';
import Icon from './Icon.vue';
import ButtonGroup from './ui/button-group/ButtonGroup.vue';
import MarkdownEditor from './MarkdownEditor.vue';

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
        <MarkdownEditor
            v-if="isEditing"
            :disabled
            :id
            v-model="draft"
        >

            <template #buttons>
                <ButtonGroup>
                    <Button size="icon-sm" variant="default" :disabled type="button" @click="save">
                        <Icon icon="ai-save" />
                    </Button>

                    <Button size="icon-sm" variant="outline" :disabled type="button" @click="cancel">
                        <Icon icon="ai-cross" />
                    </Button>
                </ButtonGroup>
            </template>

            <template #title>
                <slot name="title" />
            </template>
        </MarkdownEditor>

        <div v-else class="markdown-area-body">
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
    min-height: 300px;

    .markdown-area-body {
        flex: 1;
        display: flex;
        flex-direction: column;
        position: relative;

        .markdown {
            height: 100%;
            font-size: 10pt;
            overflow-y: auto;
        }

        .markdown-area-body-actions-wrapper {
            height: 100%;
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
