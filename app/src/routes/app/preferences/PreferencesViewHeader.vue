<template>
    <div class="preferences-view-header">
        <h1>
            <slot name="preferences-header" />
        </h1>

        <p>
            <slot name="preferences-description" />
        </p>
    </div>
</template>

<style>
.preferences-view-header {
    flex: none;
    margin-bottom: 1rem;

    > h1 {
        font-weight: 700;
        font-size: 20pt;
        margin-bottom: 0.4rem;
    }

    > p {
        font-size: 11pt;
        font-weight: 500;
        color: var(--muted-foreground);
    }
}
</style>
