import type { ClassValue } from "clsx"
import { clsx } from "clsx"
import { twMerge } from "tailwind-merge"
import { computed } from 'vue';

export function cn(...inputs: ClassValue[]) {
    return twMerge(clsx(inputs))
}

export function useModifierKey() {
    const isMac = /Mac|iPod|iPhone|iPad/.test(navigator.platform);
    const modifier = computed(() => (isMac ? '⌘' : 'Ctrl'));

    return { modifier };
}
