import { Component } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { StepsModule } from 'primeng/steps';
import { MenuItem } from 'primeng/api';
import { PromptSelectorComponent } from '../prompt-selector/prompt-selector.component';
import { PortraitMakerComponent } from '../portrait-maker/portrait-maker.component';
import { WebcamImage } from 'ngx-webcam';

@Component({
  selector: 'esp-artgen-page',
  standalone: true,
  imports: [
    ButtonModule,
    StepsModule,
    PromptSelectorComponent,
    PortraitMakerComponent],
  templateUrl: './artgen-page.component.html',
  styleUrl: './artgen-page.component.scss'
})
export class ArtgenPageComponent{
  webcamImage: WebcamImage | null = null;
  items: MenuItem[] = [
    {label: 'Select prompt'},
    {label: 'Create picture'},
    {label: 'Download'}
  ];
  activeIndex: number = 0;

  onActiveIndexChange(event: number) {
    console.log(event);
    this.activeIndex = event;
  }

  handleImage(image: WebcamImage) {
    this.webcamImage = image;
  }
}