<script lang="ts">
    import * as Avatar from "$lib/components/ui/avatar";
    import * as NavigationMenu from "$lib/components/ui/navigation-menu/index.js";
	import favicon_P from '$lib/assets/favicon.png';
    import { _ } from 'svelte-i18n';
    import Icon from "$lib/components/ui/Icon.svelte";

	let menuRef: HTMLElement | null = $state(null);
	let menuPosClass = $state('[&_div.absolute]:right-auto [&_div.absolute]:left-0');
    
	function correctMenuPos() {
		if (!menuRef)
            return;

		const menuRect = menuRef.getBoundingClientRect();
		const spaceOnLeft = menuRect.left;
		const spaceOnRight = window.innerWidth - menuRect.right;

		if (spaceOnLeft > spaceOnRight) {
			menuPosClass = '[&_div.absolute]:left-auto [&_div.absolute]:right-0';
		} else {
			menuPosClass = '[&_div.absolute]:right-auto [&_div.absolute]:left-0';
		}
	}

	$effect(() => {
		if (menuRef) {
			correctMenuPos();
		}
	});
</script>

<NavigationMenu.Root bind:ref={menuRef} class={menuPosClass}>
    <NavigationMenu.List>
        <NavigationMenu.Item class="nav-menu-button-wrapper">
            <NavigationMenu.Trigger class="nav-menu-button">
                <Avatar.Root class="nav-menu-avatar">
                    <Avatar.Image src={favicon_P} alt="User Avatar" />
                    <Avatar.Fallback>U</Avatar.Fallback>
                </Avatar.Root>
            </NavigationMenu.Trigger>

            <NavigationMenu.Content>
                <ul class="grid w-[300px] gap-4 p-2">
                    <li>
                        <NavigationMenu.Link href="/app/account/" class="nav-link">
                            <Icon icon="ai-gear" size="20px" />

                            <div>
                                <div class="font-medium nav-link-label">{$_("app.layout.navmenu.account")}</div>
                                <div class="text-muted-foreground">{$_("app.layout.navmenu.accountdesc")}</div>
                            </div>
                        </NavigationMenu.Link>

                        <NavigationMenu.Link href="/auth/logout" class="nav-link">
                            <Icon icon="ai-door" size="20px" />

                            <div>
                                <div class="font-medium nav-link-label">{$_("app.layout.navmenu.logout")}</div>
                                <div class="text-muted-foreground">{$_("app.layout.navmenu.logoutdesc")}</div>
                            </div>
                        </NavigationMenu.Link>
                    </li>
                </ul>
            </NavigationMenu.Content>
        </NavigationMenu.Item>
    </NavigationMenu.List>
</NavigationMenu.Root>

<style lang="scss">
    @use "../../lib/components/styling/vars.scss" as c;

    :global(.nav-menu-button-wrapper) {
        display: flex;
        justify-content: center;
        align-items: center;
    }

    :global(.nav-menu-button) {
        cursor: pointer;
        height: 40px;
        width: 40px;
        padding: 0.5rem;
        border: c.$border 1px solid;
        border-radius: 25px;

        > :global(.nav-menu-avatar) {
            height: inherit;
            width: inherit;
        }

        > :global(svg) {
            display: none;
        }
    }

    :global(.nav-link) {
        display: flex;
        flex-direction: row;
        align-items: center;
        gap: 0.7rem;
        justify-content: start;
    }
</style>
