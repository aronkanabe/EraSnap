import { Component, EventEmitter, Output} from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { Observable, Subject } from 'rxjs';
import { WebcamImage, WebcamModule } from 'ngx-webcam';
import { Howl } from 'howler';

@Component({
  selector: 'esp-portrait-maker',
  standalone: true,
  imports: [
    WebcamModule,
    ButtonModule],
  templateUrl: './portrait-maker.component.html',
  styleUrl: './portrait-maker.component.scss'
})
export class PortraitMakerComponent {
  @Output() imageEmitter = new EventEmitter<WebcamImage>();

  private trigger: Subject<void> = new Subject<void>();
  webcamImage: WebcamImage | null = null;
  timeLeft: number = 3;
  interval: any;
  sound: Howl = new Howl({
    src: ['assets/shutter_sound.mp3']
  });
  countDownInProgress: boolean = false;
 
  public get triggerObservable(): Observable<void> {
    return this.trigger.asObservable();
  }

  public handleImage(webcamImage: WebcamImage): void {
    console.info('received webcam image', webcamImage);
    this.webcamImage = webcamImage;
  }

  public retakeImage(): void {
    this.webcamImage = null;
  }

  public acceptImage(): void {
    if(this.webcamImage) {
      this.imageEmitter.emit(this.webcamImage);
    }
  }

  public startTimer() {
    this.countDownInProgress = true;
    this.interval = setInterval(() => {
      if(this.timeLeft > 0) {
        this.timeLeft--;
      } else {
        this.reseteTimer()
        this.takePicture();
      }
    },1000)
  }

  private reseteTimer() {
    this.countDownInProgress = false;
    clearInterval(this.interval);
    this.timeLeft = 3;
  }

  private takePicture() {
    this.sound.play();
    this.trigger.next();
  }
}