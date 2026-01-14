<script setup lang="ts">
import Button from './ui/button/Button.vue';
import VueMarkdown from 'vue-markdown-render'
import Textarea from './ui/textarea/Textarea.vue';
import { ref, watch } from 'vue';
import { useI18n } from 'vue-i18n';
import Icon from './Icon.vue';

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

function startEditing() {
    if (props.disabled)
        return

    draft.value = props.modelValue
    isEditing.value = true
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
        <div class="markdown-area-header" v-if="isEditing">
            <div class="markdown-area-header-title">
                <slot name="title" />
            </div>

            <div class="markdown-area-header-actions">
                <Button size="sm" variant="secondary" :disabled type="button" @click="cancel">
                    <Icon icon="ai-check" />

                    {{ t('components.markdownArea.cancel') }}
                </Button>

                <Button size="sm" variant="default" :disabled type="button" @click="save">
                    <Icon icon="ai-check" />

                    {{ t('components.markdownArea.save') }}
                </Button>
            </div>
        </div>

        <div v-if="isEditing" class="markdown-area-body">
            <Textarea :disabled :id v-model="draft" />
        </div>

        <div v-else class="markdown-area-body rendered" @click="startEditing()">
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
        border-bottom: 1px solid var(--border);

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
            padding: 0 0.5rem;
            overflow-y: auto;
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
  background: #f4f4f5;
  padding: 0.15rem 0.35rem;
  border-radius: 4px;
}

.markdown pre {
  background: #f4f4f5;
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
