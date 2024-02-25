import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ArtgenService } from '../artgen.service';
import { GenderPrompt, GeneratedImage, Prompt } from '../artgen.model';
import { WebcamImage } from 'ngx-webcam';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'esp-image-generator',
  standalone: true,
  imports: [
    ProgressSpinnerModule,
    ButtonModule
  ],
  templateUrl: './image-generator.component.html',
  styleUrl: './image-generator.component.scss'
})
export class ImageGeneratorComponent implements OnInit{
  @Input() prompt: GenderPrompt | null = null;
  @Input() portrait: WebcamImage | null = null;
  @Output() generateImageEmitter = new EventEmitter<GeneratedImage>();

  generatedImage: GeneratedImage | null = null;
  loading = false;

  constructor(protected artgenService: ArtgenService) {}
  
  ngOnInit(): void {
    this.loading = true;
    if(this.prompt && this.portrait) {
      console.log("Generate")
      this.artgenService.generateImage(this.prompt, this.portrait).subscribe((res) => {
        this.generatedImage = res.body
        this.loading = false;
      })
    }
  }

  buyImage() {
    if(this.generatedImage) {
      this.generateImageEmitter.emit(this.generatedImage);
    }
  }
  
}