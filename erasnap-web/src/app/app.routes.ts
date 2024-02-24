import { Routes } from '@angular/router';
import { ArtgenPageComponent } from './artgen/artgen-page/artgen-page.component';
import { DonwloadPageComponent } from './artgen/download-page/download-page.component';
import { HomePageComponent } from './artgen/home-page/home-page.component';

export const routes: Routes = [
    {
        path: '',
        component: HomePageComponent
    },
    {
        path: 'generate',
        component: ArtgenPageComponent
    },
    {
        path: 'download',
        component: DonwloadPageComponent
    }
];
