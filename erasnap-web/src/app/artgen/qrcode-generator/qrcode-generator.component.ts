import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ArtgenService } from '../artgen.service';
import { QRCodeModule } from 'angularx-qrcode';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'esp-qrcode-generator',
  standalone: true,
  imports: [
    QRCodeModule,
    ButtonModule
  ],
  templateUrl: './qrcode-generator.component.html',
  styleUrl: './qrcode-generator.component.scss'
})
export class QrcodeGeneratorComponent implements OnInit{
  @Input() imageId: string | null = null;
  downloadAddress: string | null = null;
  
  ngOnInit(): void {
    this.downloadAddress =  `${window.location.host}/download?imageId=${this.imageId}`
    console.log(window.location.host);
  }

  reset() {
    window.location.reload();
  }
  
}