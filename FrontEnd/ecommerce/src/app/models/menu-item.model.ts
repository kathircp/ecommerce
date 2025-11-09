export interface MenuItem {
    label: string;
    action?: () => void; // Optional: for actions to perform on click
    routerLink?: string; // Optional: for navigation
}
