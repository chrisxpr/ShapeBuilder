import {
  Component,
  OnInit,
  ViewChild,
  ElementRef,
  Inject,
} from '@angular/core';
import { AbstractControl, FormBuilder, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  shapeForm = this.formBuilder.group({
    definition: ['', Validators.required],
  });
  submitted = false;
  loading = false;
  invalidSearch = false;
  errorMessage = '';
  baseUrl = '';
  shapeData: ShapeData = {} as ShapeData;

  constructor(
    private formBuilder: FormBuilder,
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.baseUrl = baseUrl;
  }

  /** Template reference to the canvas element */
  /** Canvas 2d context */
  private context: CanvasRenderingContext2D = {} as CanvasRenderingContext2D;
  @ViewChild('canvasEl')
  private canvasEl: ElementRef = {} as ElementRef;

  ngOnInit(): void {}

  ngAfterViewInit() {
    this.context = this.canvasEl.nativeElement.getContext('2d');
  }

  private clearCanvas() {
    this.context.clearRect(
      0,
      0,
      (this.canvasEl.nativeElement as HTMLCanvasElement).width,
      (this.canvasEl.nativeElement as HTMLCanvasElement).height
    );
  }

  private drawRectangle(point: any) {
    this.clearCanvas();
    let width = 0;
    let height = 0;

    if (point.d['width'] && point.d['height']) {
      width = point.d['width'];
      height = point.d['height'];
    }

    if (point.d['side']) {
      width = point.d['side'];
      height = point.d['side'];
    }

    const x =
      ((this.canvasEl.nativeElement as HTMLCanvasElement).width - width) / 2;
    const y =
      ((this.canvasEl.nativeElement as HTMLCanvasElement).height - height) / 2;
    this.context.strokeRect(x, y, width, height);
  }

  private drawPointArray(points: any, type: string) {
    this.clearCanvas();
    const ratio = type == 'custom' ? 3 : 2;
    const x = (this.canvasEl.nativeElement as HTMLCanvasElement).width / ratio;
    const y = (this.canvasEl.nativeElement as HTMLCanvasElement).height / ratio;

    this.context.beginPath();

    points.map((i: any) => {
      this.context.lineTo(i.x + x, i.y + y);
    });

    this.context.lineTo(points[0].x + x, points[0].y + y);

    this.context.stroke();
  }

  private drawCircle(point: any) {
    this.clearCanvas();
    const x = (this.canvasEl.nativeElement as HTMLCanvasElement).width / 2;
    const y = (this.canvasEl.nativeElement as HTMLCanvasElement).height / 2;

    this.context.beginPath();
    this.context.arc(x, y, point.r1, 0, Math.PI * 2, true); // Outer circle
    this.context.stroke();
  }

  private drawEllipse(point: any) {
    this.clearCanvas();
    const x = (this.canvasEl.nativeElement as HTMLCanvasElement).width / 2;
    const y = (this.canvasEl.nativeElement as HTMLCanvasElement).height / 2;
    this.context.beginPath();
    this.context.ellipse(x, y, point.r1, point.r2, 0, 0, 2 * Math.PI);
    this.context.stroke();
  }

  get f(): { [key: string]: AbstractControl } {
    return this.shapeForm.controls;
  }

  onSubmit(): void {
    this.submitted = true;
    this.loading = true;
    this.invalidSearch = false;
    this.errorMessage = '';
    this.clearCanvas();

    console.log();

    this.http
      .get<ShapeData>(
        this.baseUrl + 'shape/' + encodeURI(this.f.definition.value)
      )
      .subscribe(
        (result) => {
          console.log('this.shapeData' + JSON.stringify(result));

          this.shapeData = result;
          if (!this.shapeData.match) {
            this.invalidSearch = true;
            this.errorMessage = this.shapeData.message;

            return;
          }

          if (
            this.shapeData.type === 'pointarray' ||
            this.shapeData.type === 'custom'
          ) {
            this.drawPointArray(this.shapeData.dataPoints, this.shapeData.type);
          } else if (this.shapeData.type === 'circle') {
            this.drawCircle(this.shapeData.dataPoints[0]);
          } else if (this.shapeData.type === 'ellipse') {
            this.drawEllipse(this.shapeData.dataPoints[0]);
          } else if (this.shapeData.type === 'rectangle') {
            this.drawRectangle(this.shapeData.dataPoints[0]);
          }
        },
        (error) => console.error(error)
      );

    this.loading = false;
  }

  onReset(): void {
    this.submitted = false;
    this.invalidSearch = false;
    this.shapeForm.reset();
  }
}

interface ShapeData {
  name: string;
  type: string;
  match: boolean;
  message: string;
  dataPoints: Array<DataPoint>;
}

interface DataPoint {
  x: number;
  y: number;
  r1: number;
  r2: number;
  d: any;
}
