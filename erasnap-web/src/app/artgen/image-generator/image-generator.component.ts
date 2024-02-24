import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ArtgenService } from '../artgen.service';
import { GeneratedImage, Prompt } from '../artgen.model';
import { WebcamImage } from 'ngx-webcam';
import { ProgressSpinnerModule } from 'primeng/progressspinner';

@Component({
  selector: 'esp-image-generator',
  standalone: true,
  imports: [
    ProgressSpinnerModule,
  ],
  templateUrl: './image-generator.component.html',
  styleUrl: './image-generator.component.scss'
})
export class ImageGeneratorComponent implements OnInit{
  @Input() prompt: Prompt | null = null;
  @Input() portrait: WebcamImage | null = null;
  @Output() generateImageEmitter = new EventEmitter<GeneratedImage>();

  generatedImage: GeneratedImage | null = null;
  loading = false;

  constructor(protected artgenService: ArtgenService) {}
  
  ngOnInit(): void {
    this.loading = true;
    // this.artgenService.queryPrompts().subscribe((res) => {
    //   this.loading = false;
    // })
  }
  
}