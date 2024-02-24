import { Component, OnInit } from '@angular/core';
import { ArtgenService } from '../artgen.service';
import { Prompt } from '../artgen.model';
import { ListboxModule } from 'primeng/listbox';
import {InputTextareaModule} from 'primeng/inputtextarea';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { DomSanitizer } from '@angular/platform-browser';
import { WebcamModule } from 'ngx-webcam';
import { CarouselModule } from 'primeng/carousel';

@Component({
  selector: 'esp-prompt-selector',
  standalone: true,
  imports: [
    ListboxModule,
    CardModule,
    InputTextareaModule,
    WebcamModule,
    ButtonModule,
    CarouselModule],
  templateUrl: './prompt-selector.component.html',
  styleUrl: './prompt-selector.component.scss'
})
export class PromptSelectorComponent implements OnInit{
  prompts: Prompt[] = [];
  selectedPrompt: Prompt | undefined;
  // [...res.body, {'name': 'Custom'}]
 
  constructor(protected artgenService: ArtgenService,
    private sanitizer: DomSanitizer) {}

  ngOnInit(): void {
    this.artgenService.query().subscribe((res) => {
      this.prompts = res.body ?? []
    })
  }
}