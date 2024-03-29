import { Component, ElementRef, ViewChild } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { StepsModule } from 'primeng/steps';
import { MenuItem } from 'primeng/api';
import { PortraitMakerComponent } from '../portrait-maker/portrait-maker.component';
import { WebcamImage } from 'ngx-webcam';
import { QrcodeGeneratorComponent } from '../qrcode-generator/qrcode-generator.component';
import { PromptSelectorComponent } from '../prompt-selector/prompt-selector.component';
import { GenderPrompt, GeneratedImage, Prompt } from '../artgen.model';
import { ImageGeneratorComponent } from '../image-generator/image-generator.component';

@Component({
  selector: 'esp-artgen-page',
  standalone: true,
  imports: [
    ButtonModule,
    StepsModule,
    PromptSelectorComponent,
    PortraitMakerComponent,
    QrcodeGeneratorComponent,
    ImageGeneratorComponent],
  templateUrl: './artgen-page.component.html',
  styleUrl: './artgen-page.component.scss'
})
export class ArtgenPageComponent{
  items: MenuItem[] = [
    {label: 'Choosing the theme'},
    {label: 'Taking the picture'},
    {label: 'Image generation'},
    {label: 'Download'}
  ];
  activeIndex: number = 0;

  portrait: WebcamImage | null = null;
  selectedPrompt: GenderPrompt | null = null;
  generatedImage: GeneratedImage | null = null;

  public onActiveIndexChange(event: number) {
    this.activeIndex = event;
  }

  public setPrompt(genderPrompt: GenderPrompt) {
    this.selectedPrompt = genderPrompt;
    console.log(genderPrompt.prompt?.name);
    ++this.activeIndex;
  }

  public setPortrait(webcamImage: WebcamImage) {
    this.portrait = webcamImage;
    ++this.activeIndex;
  }

  public setGeneratedImage(image: GeneratedImage) {
    this.generatedImage = image;
    ++this.activeIndex;
  }
}