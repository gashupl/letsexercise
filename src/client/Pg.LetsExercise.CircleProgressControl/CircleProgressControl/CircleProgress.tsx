import * as React from 'react';
import { Label } from '@fluentui/react';
import { Progress } from 'rsuite';

const style = {
  width: 120,
  display: 'inline-block',
  marginRight: 10
};

export interface ICircleProgressProps {
  percentage?: number;
}

export class CircleProgress extends React.Component<ICircleProgressProps> {
  public render(): React.ReactNode {
    if(!this.props.percentage){
      return (  
        <div style={style}>
          <Progress.Circle percent={0} strokeColor="#ffc107" />
        </div>
      ); 
    }
    else if(this.props.percentage && this.props.percentage < 100){
      return (  
        <div style={style}>
          <Progress.Circle percent={this.props.percentage} strokeColor="#ffc107" />
        </div>
      ); 
    }
    else{
      return (  
        <div style={style}>
          <Progress.Circle percent={this.props.percentage} status="success" />
        </div>
      ); 
    }

  }
}
