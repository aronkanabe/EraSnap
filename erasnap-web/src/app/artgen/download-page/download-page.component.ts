import { Component, OnInit } from '@angular/core';
import { ArtgenService } from '../artgen.service';
import { GeneratedImage } from '../artgen.model';
import { ActivatedRoute } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { FileSaverModule, FileSaverService } from 'ngx-filesaver';

@Component({
  selector: 'esp-download-page',
  standalone: true,
  imports: [ButtonModule, FileSaverModule],
  templateUrl: './download-page.component.html',
  styleUrl: './download-page.component.scss'
})
export class DonwloadPageComponent implements OnInit{
  image: GeneratedImage | null = null;
 
  constructor(protected artgenService: ArtgenService,
    private route: ActivatedRoute,
    private fileSaverService: FileSaverService) {}

  ngOnInit(): void {
    let imageId = this.route.snapshot.queryParamMap.get('imageId');
    console.log(imageId);
    if(imageId) {
      this.artgenService.findGeneratedImage(imageId).subscribe(async (res) => {
      this.image = res.body;
      });
    }
  }

  saveImage(): void {
    if(this.image?.image) {
      const byteCharacters = atob(this.image.image);
      const byteNumbers = new Array(byteCharacters.length);
      for (let i = 0; i < byteCharacters.length; i++) {
        byteNumbers[i] = byteCharacters.charCodeAt(i);
      }
      const byteArray = new Uint8Array(byteNumbers);
      const blob = new Blob([byteArray], { type: 'image/png' });
      this.fileSaverService.save(blob, this.image.id);
    }
  }
}