import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { ArtgenService } from '../artgen.service';
import { GenderPrompt, Prompt } from '../artgen.model';
import { GalleryImageDef, GalleryItem, GalleryModule, ImageItem, GalleryComponent, GalleryState } from 'ng-gallery';
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
  @ViewChild('gallery') gallery?: GalleryComponent;
  @Output() promptEmitter = new EventEmitter<GenderPrompt>();
  prompts: Prompt[] = [];
  selectedPrompt: Prompt | null = null;
  images: GalleryItem[] = [];
  gender: string = "male";
  index: number = 0;

  constructor(protected artgenService: ArtgenService) {}
  
  ngOnInit(): void {
    this.artgenService.queryPrompts().subscribe((res) => {
      this.prompts = res.body ?? [];
      this.images = this.prompts.map((prompt) => new ImageItem({ src: `data:image/jpg;base64,${prompt.image}`, thumb: `data:image/jpg;base64,${prompt.image}`, alt: prompt.name }))
    })
  }

  public selectPrompt() {
    this.promptEmitter.emit({prompt: this.prompts[this.index], gender: this.gender});
  }

  public indexChange (newIndex: GalleryState) {
    this.index = newIndex.currIndex ? newIndex.currIndex : 0;
  }
  
}