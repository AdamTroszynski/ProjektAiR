$(function(){
			$.get("get_pixel.php", function(data, status){
				var LEDlist = data;
				var row;
				var col;
				for (var i=0; i < LEDlist.length; i++) {
				
					var RGBarray = LEDlist[i];
					var R = RGBarray[0];
					var G = RGBarray[1];
					var B = RGBarray[2];
					var colorcode = "#"+R.toString(16).padStart(2,'0')+G.toString(16).padStart(2,'0')+B.toString(16).padStart(2,'0')

					var color_input = $("<input>",{
						id: "led_value_"+i, 
						title: "led"+i, 
						type: "color", 
						"class":"btn btn-default form-control p-0"
					});
					color_input.val(colorcode);
					color_input.change(function(){
						var led_id = $(this).attr('id').split('_')[2];
						var led_x = led_id % 8;
						var led_y = Math.floor(led_id/8);
						var color_hex_text = $(this).val();
						var color_integer = parseInt(color_hex_text.substring(1), 16);
						var R = (color_integer & 0xff0000) >> 16;
						var G = (color_integer & 0x00ff00) >> 8;
						var B = color_integer & 0x0000ff;
						var url = "set_pixel.php?x="+led_x+"&y="+led_y+"&r="+R+"&g="+G+"&b="+B;						
						$.get(url, function(data, status){	        
							$("#response").text(data);							
						})  .done(function(data) {
							$("#response").text("Response: "+data);
						  })
						  .fail(function() {
							$("#response").html("Request with error. Check connection.<br>");
						  })
						  .always(function() {
							$("#request").html(url);
						  });
					});
					
					if(i%8==0){
						var row = $("<div>",{"class":"row mb-1"});
					}
					var col = $("<div>",{"class":"col-1 p-1", "id":"led_div_"+i});					
					var input_group = $("<div>",{"class":"input-group w-50"});
					var input_prepend = $("<div>",{"class":"input-group-prepend"});
					var input_append = $("<div>",{"class":"input-group-append"});
				
					input_group.append(input_prepend);
					input_group.append(color_input);
					input_group.append(input_append);
					
					col.append(input_group);
					row.append(col);
					$("#main").append(row);					

				
				}
			}, "json").fail(function() {
			toastr.warning("Fail of HTTP GET request.");
			});
			
			$("#search").keyup(function(){
				var filter_text = $(this).val().toUpperCase();
				$("[id^='led_div']").each( function(){
					if($(this).html().toUpperCase().indexOf(filter_text)  > -1){
						$(this).removeClass("btn-secondary");
					}else{
						$(this).addClass("btn-secondary");
					}
				});
			});

		});
