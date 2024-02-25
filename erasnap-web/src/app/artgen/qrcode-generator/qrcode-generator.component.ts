import { Component, Input, OnInit } from '@angular/core';
import { ArtgenService } from '../artgen.service';
import { QRCodeModule } from 'angularx-qrcode';

@Component({
  selector: 'esp-qrcode-generator',
  standalone: true,
  imports: [
    QRCodeModule
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
  
}