import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ArtgenService } from '../artgen.service';
import { Prompt } from '../artgen.model';
import { GalleryImageDef, GalleryItem, GalleryModule, ImageItem } from 'ng-gallery';
import { ButtonModule } from 'primeng/button';
import { RadioButtonModule } from 'primeng/radiobutton';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'esp-prompt-selector',
  standalone: true,
  imports: [
    GalleryModule,
    ButtonModule,
    RadioButtonModule,
    FormsModule
  ],
  templateUrl: './prompt-selector.component.html',
  styleUrl: './prompt-selector.component.scss'
})
export class PromptSelectorComponent implements OnInit{
  @Output() promptEmitter = new EventEmitter<Prompt>();
  prompts: Prompt[] = [];
  selectedPrompt: Prompt | null = null;
  images: GalleryItem[] = [];
  gender: string = "male";

  constructor(protected artgenService: ArtgenService) {}
  
  ngOnInit(): void {
    this.artgenService.queryPrompts().subscribe((res) => {
      this.prompts = res.body ?? [];
      this.images = this.prompts.map((prompt) => new ImageItem({ src: `data:image/jpg;base64,${prompt.image}`, thumb: `data:image/jpg;base64,${prompt.image}` }))
    })
  }

  public selectPrompt() {
    this.promptEmitter.emit(this.prompts[0]);
  }
  
}