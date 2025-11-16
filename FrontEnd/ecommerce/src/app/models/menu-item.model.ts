import { MatMenuPanel } from "@angular/material/menu";

export interface MenuItem {
    label: string;
    action?: () => void; // Optional: for actions to perform on click
    routerLink?: string; // Optional: for navigation
    matMenuTriggerFor?: MatMenuPanel<any>; // Optional: for Angular Material menu trigger
}
