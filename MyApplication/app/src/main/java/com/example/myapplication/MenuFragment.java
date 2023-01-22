package com.example.myapplication;

import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;

public class MenuFragment extends Fragment {
    EditText ipEditText;
    EditText sampleEditText;
    Button button;
    public String IP_ADDRESS;
    public String SAMPLES;
    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.fragment_menu, container, false);
        Bundle arg = new Bundle();
        ipEditText = v.findViewById(R.id.ipEditTextConfig);
        sampleEditText = v.findViewById(R.id.sampleTimeEditTextConfig);
        button = v.findViewById(R.id.button);
        button.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                IP_ADDRESS = ipEditText.getText().toString();
                GlobalVar.CONFIG_IP_ADDRESS = IP_ADDRESS;

                SAMPLES = sampleEditText.getText().toString();
                GlobalVar.CONFIG_SAMPLE_TIME = SAMPLES;
            }
        });
        ipEditText.setText(GlobalVar.CONFIG_IP_ADDRESS);
        sampleEditText.setText(GlobalVar.CONFIG_SAMPLE_TIME);
        return v;
    }
}
